using System;

namespace CarFinder.Logic
{
    public class Car : ICar
    {
        private short _initialPosition;
        private long _currentPosition;
        private short _initialVelocity;
        private uint _tick;

        public long CurrentPosition
        {
            get { return _currentPosition; }
            set { _currentPosition = value; }
        }

        public uint Tick
        {
            get { return _tick; }
            private set { _tick = value; }
        }

        public Car(short initialVelocity, short initialPosition)
        {
            SetInitialVelocity(initialVelocity);
            SetInitialPosition(initialPosition);
        }

        private void SetInitialPosition(short value)
        {
            if (IsValidPosition(value))
            {
                _initialPosition = value;
                _currentPosition = value;
            }
            else
            {
                throw new Exception("Invalid Position"); // can be refactored to use custom exceptions
            }
        }

        private void SetInitialVelocity(short value)
        {
            if (IsValidVelocity(value))
            {
                _initialVelocity = value;
            }
            else
            {
                throw new Exception("Invalid Velocity"); // can be refactored to use custom exceptions
            }
        }

        private long ComputeCurrentPosition(int velocity)
        {
            // tick is uint to keep it non-negative
            return _initialPosition + (_tick * velocity);
        }

        private bool IsValidVelocity(int velocity)
        {
            return CustomValidationRule(velocity);
        }

        private bool IsValidPosition(int position)
        {
            return CustomValidationRule(position);
        }

        public void MoveCar(uint tick, int velocity)
        {
            _tick = tick;
            _currentPosition = ComputeCurrentPosition(velocity);
        }

        private bool CustomValidationRule(int value)
        {
            // can refactor to have custom rules engine  
            return value >= -1000 && value <= 1000;
        }

    }
}