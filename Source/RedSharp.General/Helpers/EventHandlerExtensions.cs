using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RedSharp.General.Helpers
{
    public static class EventHandlerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeSafe(this EventHandler @event, object sender)
        {
            if (@event != null)
            {
                try
                {
                    @event.Invoke(sender, EventArgs.Empty);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeSafe<TArgument>(this EventHandler<TArgument> @event, object sender, Func<TArgument> argumentsCreator)
        {
            if (@event != null)
            {
                try
                {
                    @event.Invoke(sender, argumentsCreator.Invoke());
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeSafe(this PropertyChangingEventHandler @event, object sender, string property)
        {
            if (@event != null && property != null)
            {
                try
                {
                    var arguments = new PropertyChangingEventArgs(property);

                    @event.Invoke(sender, arguments);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeSafe(this PropertyChangingEventHandler @event, object sender, PropertyChangingEventArgs arguments)
        {
            if (@event != null)
            {
                try
                {
                    @event.Invoke(sender, arguments);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeSafe(this PropertyChangedEventHandler @event, object sender, string property)
        {
            if (@event != null && property != null)
            {
                try
                {
                    var arguments = new PropertyChangedEventArgs(property);

                    @event.Invoke(sender, arguments);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeSafe(this PropertyChangedEventHandler @event, object sender, PropertyChangedEventArgs arguments)
        {
            if (@event != null)
            {
                try
                {
                    @event.Invoke(sender, arguments);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }
    }
}
