using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chagrins
{
    public class BaseController : BaseBehaviour
    {
        protected Vector3 _frameStartPosition;
        protected Vector3 _frameEndPosition;

        protected bool _framePositionUpdated;

        // Use this for initialization
        void Start()
        {
            _Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            _Update();
        }

        protected virtual void _Initialize()
        {
        }

        /// <summary>
        /// The core update function. You should always call this in your Update function.
        /// Override _BeginStep, _Step and _EndStep to add additional code.
        /// </summary>
        protected void _Update()
        {
            _BeginStep();

            _Step();

            _EndStep();
        }

        /// <summary>
        /// The first part of the update. Will only be called at the beginning of the update.
        /// </summary>
        protected virtual void _BeginStep()
        {
            _frameStartPosition = transform.position;
        }

        /// <summary>
        /// The update function. This is the main function you'll want to override for each class.
        /// </summary>
        protected virtual void _Step() { }

        /// <summary>
        /// The final part of the update. Will only be called at the end of the update.
        /// </summary>
        protected virtual void _EndStep() { }
    }
}