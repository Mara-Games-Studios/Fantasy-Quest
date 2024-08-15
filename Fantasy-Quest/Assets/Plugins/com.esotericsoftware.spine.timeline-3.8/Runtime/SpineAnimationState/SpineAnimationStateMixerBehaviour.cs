/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated January 1, 2020. Replaces all prior versions.
 *
 * Copyright (c) 2013-2020, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

#define SPINE_EDITMODEPOSE

using UnityEngine;
using UnityEngine.Playables;

namespace Spine.Unity.Playables
{
    public class SpineAnimationStateMixerBehaviour : PlayableBehaviour
    {
        private float[] lastInputWeights;
        public int trackIndex;

        // NOTE: This function is called at runtime and edit time. Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SkeletonAnimation skeletonAnimation = playerData as SkeletonAnimation;
            SkeletonGraphic skeletonGraphic = playerData as SkeletonGraphic;
            IAnimationStateComponent animationStateComponent =
                playerData as IAnimationStateComponent;
            ISkeletonComponent skeletonComponent = playerData as ISkeletonComponent;
            if (animationStateComponent == null || skeletonComponent == null)
            {
                return;
            }

            Skeleton skeleton = skeletonComponent.Skeleton;
            AnimationState state = animationStateComponent.AnimationState;

            if (!Application.isPlaying)
            {
#if SPINE_EDITMODEPOSE
                PreviewEditModePose(
                    playable,
                    skeletonComponent,
                    animationStateComponent,
                    skeletonAnimation,
                    skeletonGraphic
                );
#endif
                return;
            }

            int inputCount = playable.GetInputCount();

            // Ensure correct buffer size.
            if (this.lastInputWeights == null || this.lastInputWeights.Length < inputCount)
            {
                this.lastInputWeights = new float[inputCount];

                for (int i = 0; i < inputCount; i++)
                {
                    this.lastInputWeights[i] = default;
                }
            }
            float[] lastInputWeights = this.lastInputWeights;

            // Check all clips. If a clip that was weight 0 turned into weight 1, call SetAnimation.
            for (int i = 0; i < inputCount; i++)
            {
                float lastInputWeight = lastInputWeights[i];
                float inputWeight = playable.GetInputWeight(i);
                bool trackStarted = lastInputWeight == 0 && inputWeight > 0;
                lastInputWeights[i] = inputWeight;

                if (trackStarted)
                {
                    ScriptPlayable<SpineAnimationStateBehaviour> inputPlayable =
                        (ScriptPlayable<SpineAnimationStateBehaviour>)playable.GetInput(i);
                    SpineAnimationStateBehaviour clipData = inputPlayable.GetBehaviour();

                    if (clipData.animationReference == null)
                    {
                        float mixDuration = clipData.customDuration
                            ? clipData.mixDuration
                            : state.Data.DefaultMix;
                        state.SetEmptyAnimation(trackIndex, mixDuration);
                    }
                    else
                    {
                        if (clipData.animationReference.Animation != null)
                        {
                            Spine.TrackEntry trackEntry = state.SetAnimation(
                                trackIndex,
                                clipData.animationReference.Animation,
                                clipData.loop
                            );

                            trackEntry.EventThreshold = clipData.eventThreshold;
                            trackEntry.DrawOrderThreshold = clipData.drawOrderThreshold;
                            trackEntry.TrackTime =
                                (float)inputPlayable.GetTime() * (float)inputPlayable.GetSpeed();
                            trackEntry.TimeScale = (float)inputPlayable.GetSpeed();
                            trackEntry.AttachmentThreshold = clipData.attachmentThreshold;
                            trackEntry.HoldPrevious = clipData.holdPrevious;

                            if (clipData.customDuration)
                            {
                                trackEntry.MixDuration = clipData.mixDuration;
                            }
                        }
                        //else Debug.LogWarningFormat("Animation named '{0}' not found", clipData.animationName);
                    }

                    // Ensure that the first frame ends with an updated mesh.
                    if (skeletonAnimation)
                    {
                        skeletonAnimation.UpdateInternal(0);
                        skeletonAnimation.LateUpdate();
                    }
                    else if (skeletonGraphic)
                    {
                        skeletonGraphic.UpdateInternal(0);
                        skeletonGraphic.LateUpdate();
                    }
                }
            }
        }

#if SPINE_EDITMODEPOSE

        private AnimationState dummyAnimationState;

        public void PreviewEditModePose(
            Playable playable,
            ISkeletonComponent skeletonComponent,
            IAnimationStateComponent animationStateComponent,
            SkeletonAnimation skeletonAnimation,
            SkeletonGraphic skeletonGraphic
        )
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (skeletonComponent == null || animationStateComponent == null)
            {
                return;
            }

            int inputCount = playable.GetInputCount();
            int lastNonZeroWeightTrack = -1;

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0)
                {
                    lastNonZeroWeightTrack = i;
                }
            }

            if (lastNonZeroWeightTrack != -1)
            {
                ScriptPlayable<SpineAnimationStateBehaviour> inputPlayableClip =
                    (ScriptPlayable<SpineAnimationStateBehaviour>)
                        playable.GetInput(lastNonZeroWeightTrack);
                SpineAnimationStateBehaviour clipData = inputPlayableClip.GetBehaviour();

                Skeleton skeleton = skeletonComponent.Skeleton;

                bool skeletonDataMismatch =
                    clipData.animationReference != null
                    && clipData.animationReference.SkeletonDataAsset
                    && skeletonComponent.SkeletonDataAsset.GetSkeletonData(true)
                        != clipData.animationReference.SkeletonDataAsset.GetSkeletonData(true);
                if (skeletonDataMismatch)
                {
                    Debug.LogWarningFormat(
                        "SpineAnimationStateMixerBehaviour tried to apply an animation for the wrong skeleton. Expected {0}. Was {1}",
                        skeletonComponent.SkeletonDataAsset,
                        clipData.animationReference.SkeletonDataAsset
                    );
                }

                // Getting the from-animation here because it's required to get the mix information from AnimationStateData.
                Animation fromAnimation = null;
                float fromClipTime = 0;
                bool fromClipLoop = false;
                if (lastNonZeroWeightTrack != 0 && inputCount > 1)
                {
                    ScriptPlayable<SpineAnimationStateBehaviour> fromClip =
                        (ScriptPlayable<SpineAnimationStateBehaviour>)
                            playable.GetInput(lastNonZeroWeightTrack - 1);
                    SpineAnimationStateBehaviour fromClipData = fromClip.GetBehaviour();
                    fromAnimation =
                        fromClipData.animationReference != null
                            ? fromClipData.animationReference.Animation
                            : null;
                    fromClipTime = (float)fromClip.GetTime() * (float)fromClip.GetSpeed();
                    fromClipLoop = fromClipData.loop;
                }

                Animation toAnimation =
                    clipData.animationReference != null
                        ? clipData.animationReference.Animation
                        : null;
                float toClipTime =
                    (float)inputPlayableClip.GetTime() * (float)inputPlayableClip.GetSpeed();
                float mixDuration = clipData.mixDuration;

                if (!clipData.customDuration && fromAnimation != null && toAnimation != null)
                {
                    mixDuration = animationStateComponent.AnimationState.Data.GetMix(
                        fromAnimation,
                        toAnimation
                    );
                }

                if (trackIndex == 0)
                {
                    skeleton.SetToSetupPose();
                }

                // Approximate what AnimationState might do at runtime.
                if (fromAnimation != null && mixDuration > 0 && toClipTime < mixDuration)
                {
                    dummyAnimationState ??= new AnimationState(
                        skeletonComponent.SkeletonDataAsset.GetAnimationStateData()
                    );

                    TrackEntry toTrack = dummyAnimationState.GetCurrent(0);
                    TrackEntry fromTrack = toTrack?.MixingFrom;
                    bool isAnimationTransitionMatch =
                        toTrack != null
                        && toTrack.Animation == toAnimation
                        && fromTrack != null
                        && fromTrack.Animation == fromAnimation;

                    if (!isAnimationTransitionMatch)
                    {
                        dummyAnimationState.ClearTracks();
                        fromTrack = dummyAnimationState.SetAnimation(
                            0,
                            fromAnimation,
                            fromClipLoop
                        );
                        fromTrack.AllowImmediateQueue();
                        if (toAnimation != null)
                        {
                            toTrack = dummyAnimationState.SetAnimation(
                                0,
                                toAnimation,
                                clipData.loop
                            );
                            toTrack.HoldPrevious = clipData.holdPrevious;
                        }
                    }

                    // Update track times.
                    fromTrack.TrackTime = fromClipTime;
                    if (toTrack != null)
                    {
                        toTrack.TrackTime = toClipTime;
                        toTrack.MixTime = toClipTime;
                    }

                    // Apply Pose
                    dummyAnimationState.Update(0);
                    _ = dummyAnimationState.Apply(skeleton);
                }
                else
                {
                    toAnimation?.Apply(
                        skeleton,
                        0,
                        toClipTime,
                        clipData.loop,
                        null,
                        1f,
                        MixBlend.Setup,
                        MixDirection.In
                    );
                }
                skeleton.UpdateWorldTransform();

                if (skeletonAnimation)
                {
                    skeletonAnimation.LateUpdate();
                }
                else if (skeletonGraphic)
                {
                    skeletonGraphic.LateUpdate();
                }
            }
            // Do nothing outside of the first clip and the last clip.
        }
#endif
    }
}
