using Cat.StateMachine;
using UnityEngine;

namespace Cat.Strategies.Jump
{
    public class Up : IJumpable
    {
        private Transform target;
        private StateMachineData data;
        private UpJumpConfig upJumpConfig;

        public Up(Transform targetTransform, UpJumpConfig config, StateMachineData data)
        {
            this.data = data;
            target = targetTransform;
            upJumpConfig = config;
        }

        public void Jump()
        {
            if (target.transform.localScale.x / Mathf.Abs(target.transform.localScale.x) > 0)
            {
                data.XVelocity = upJumpConfig.StartXVelocity;
            }
            else
            {
                data.XVelocity = upJumpConfig.StartXVelocity * -1;
            }

            data.YVelocity = upJumpConfig.StartYVelocity;
        }

        public void Update()
        {
            target.Translate(GetConvertedVelocity() * Time.deltaTime);
            data.YVelocity -= upJumpConfig.BaseGravity * Time.deltaTime;

            if (target.transform.localScale.x / Mathf.Abs(target.transform.localScale.x) > 0)
            {
                if (data.XVelocity > 0)
                {
                    data.XVelocity -= upJumpConfig.XMoveResistance * Time.deltaTime;
                }
                else
                {
                    data.XVelocity = 0;
                }
            }
            else
            {
                if (data.XVelocity < 0)
                {
                    data.XVelocity += upJumpConfig.XMoveResistance * Time.deltaTime;
                }
                else
                {
                    data.XVelocity = 0;
                }
            }
        }

        protected Vector2 GetConvertedVelocity()
        {
            return new Vector2(data.XVelocity, data.YVelocity);
        }
    }
}
