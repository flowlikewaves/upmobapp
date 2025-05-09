using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Mobappg4v2.Helpers
{
    public static class ToastHelper
    {
        public static async Task ShowToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new();
            string text = message;
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            cancellationTokenSource.Dispose();
        }
    }
}
