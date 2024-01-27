using Cat.StateMachine;
using UnityEngine;

namespace Cat.Strategies.Jump
{
    public class Down : IJumpable
    {
        private Transform target;
        private StateMachineData data;
        private DownJumpConfig downJumpConfig;

        public Down(Transform targetTransform, DownJumpConfig config, StateMachineData data)
        {
            target = targetTransform;
            downJumpConfig = config;
            this.data = data;
        }

        public void Jump()
        {
            if (target.transform.localScale.x / Mathf.Abs(target.transform.localScale.x) > 0)
            {
                data.XVelocity = downJumpConfig.StartXVelocity;
            }
            else
            {
                data.XVelocity = downJumpConfig.StartXVelocity * -1;
            }

            data.YVelocity = downJumpConfig.StartYVelocity;
        }

        public void Update()
        {
            target.Translate(GetConvertedVelocity() * Time.deltaTime);
            data.YVelocity -= downJumpConfig.BaseGravity * Time.deltaTime;

            if (target.transform.localScale.x / Mathf.Abs(target.transform.localScale.x) > 0)
            {
                if (data.XVelocity > 0)
                {
                    data.XVelocity -= downJumpConfig.XMoveResistance * Time.deltaTime;
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
                    data.XVelocity += downJumpConfig.XMoveResistance * Time.deltaTime;
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
