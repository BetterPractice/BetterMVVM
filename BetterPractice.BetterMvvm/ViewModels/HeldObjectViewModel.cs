using System;
namespace BetterPractice.BetterMvvm.ViewModels
{
    public class HeldObjectViewModel<T> : RowViewModel where T : class
    {
        public HeldObjectViewModel(T heldObject)
        {
            HeldObject = heldObject;
        }

        public T HeldObject { get; }
    }
}
