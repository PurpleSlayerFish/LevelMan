using System;

namespace PurpleSlayerFish.Core.Services.Input
{
    public class InputProvider : IInputProvider<InputData>
    {
        public InputProvider()
        {
            Data = new InputData();
        }

        public InputData Data { get; }
    }

    public class InputData
    {
        private bool _blockInput;
        public float VerticalAxis;
        public float HorizontalAxis;
        public Action OnEscape;
        public Action OnActionMain;
        public Action OnActionSecond;
        public Action OnAttack;
        
        public bool BlockInput
        {
            get => _blockInput;
            set
            {
                _blockInput = value;
                HorizontalAxis = default;
                VerticalAxis = default;
            }
        }
    }
}