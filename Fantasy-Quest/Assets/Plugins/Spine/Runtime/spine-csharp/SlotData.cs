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

using System;

namespace Spine
{
    public class SlotData
    {
        internal int index;
        internal string name;
        internal BoneData boneData;
        internal float r = 1,
            g = 1,
            b = 1,
            a = 1;
        internal float r2 = 0,
            g2 = 0,
            b2 = 0;
        internal bool hasSecondColor = false;
        internal string attachmentName;
        internal BlendMode blendMode;

        /// <summary>The index of the slot in <see cref="Skeleton.Slots"/>.</summary>
        public int Index => index;

        /// <summary>The name of the slot, which is unique across all slots in the skeleton.</summary>
        public string Name => name;

        /// <summary>The bone this slot belongs to.</summary>
        public BoneData BoneData => boneData;
        public float R
        {
            get => r;
            set => r = value;
        }
        public float G
        {
            get => g;
            set => g = value;
        }
        public float B
        {
            get => b;
            set => b = value;
        }
        public float A
        {
            get => a;
            set => a = value;
        }

        public float R2
        {
            get => r2;
            set => r2 = value;
        }
        public float G2
        {
            get => g2;
            set => g2 = value;
        }
        public float B2
        {
            get => b2;
            set => b2 = value;
        }
        public bool HasSecondColor
        {
            get => hasSecondColor;
            set => hasSecondColor = value;
        }

        /// <summary>The name of the attachment that is visible for this slot in the setup pose, or null if no attachment is visible.</summary>
        public string AttachmentName
        {
            get => attachmentName;
            set => attachmentName = value;
        }

        /// <summary>The blend mode for drawing the slot's attachment.</summary>
        public BlendMode BlendMode
        {
            get => blendMode;
            set => blendMode = value;
        }

        public SlotData(int index, string name, BoneData boneData)
        {
            if (index < 0)
            {
                throw new ArgumentException("index must be >= 0.", "index");
            }

            this.index = index;
            this.name = name ?? throw new ArgumentNullException("name", "name cannot be null.");
            this.boneData =
                boneData ?? throw new ArgumentNullException("boneData", "boneData cannot be null.");
        }

        public override string ToString()
        {
            return name;
        }
    }
}
