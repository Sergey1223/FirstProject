using System;
namespace LockerBox
{
    public static class EventExtension
    {
        public static void Raise<T>(this EventHandler<T> ev, object sender, T args) where T: EventArgs
        {
            var temp = ev;
            if (temp != null)
            {
                temp(sender, args);
            }
        }
    }
}
