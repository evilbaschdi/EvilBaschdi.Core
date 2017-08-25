using System;
using System.Reflection;

namespace EvilBaschdi.Core.Wpf
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for toast message support.
    /// </summary>
    public class Toast : IToast
    {
        private readonly string _applicationId;
        private readonly string _imagePath;
        private bool _showToast;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="imagePath" /> is <see langword="null" />.
        /// </exception>
        public Toast(string imagePath)
        {
            _applicationId = Assembly.GetExecutingAssembly().GetName().Name;
            _imagePath = imagePath ?? throw new ArgumentNullException(nameof(imagePath));
            ValidateOsVersion();
        }

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="imagePath" /> is <see langword="null" />.
        ///     <paramref name="applicationId" /> is <see langword="null" />.
        /// </exception>
        public Toast(string applicationId, string imagePath)
        {
            _applicationId = applicationId ?? throw new ArgumentNullException(nameof(applicationId));
            _imagePath = imagePath;
            ValidateOsVersion();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Show Message.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="status" /> is <see langword="null" />.
        ///     <paramref name="message" /> is <see langword="null" />.
        /// </exception>
        public void Show(string status, string message)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            //C:\Program Files (x86)\Windows Kits\8.1\References\CommonConfiguration\Neutral\Windows.winmd
            /*
            if (!_showToast)
            {
                return;
            }

            // Get a toast XML template
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            // Fill in the text elements
            var stringElements = toastXml.GetElementsByTagName("text");

            //stringElements[0].AppendChild(toastXml.CreateTextNode("Folders2Md5"));
            stringElements[1].AppendChild(toastXml.CreateTextNode(status));
            stringElements[2].AppendChild(toastXml.CreateTextNode(message));

            // Specify the absolute path to an image
            if (!string.IsNullOrWhiteSpace(_imagePath))
            {
                var imagePath = $"file:///{Path.GetFullPath(_imagePath)}";
                var imageElements = toastXml.GetElementsByTagName("image");
                var image = imageElements[0].Attributes.GetNamedItem("src");
                if (image != null)
                {
                    image.NodeValue = imagePath;
                }
            }


            var toastNode = toastXml.SelectSingleNode("/toast");
            var node = (XmlElement) toastNode;
            node?.SetAttribute("duration", "long");

            // Create the toast and attach event listeners
            var toast = new ToastNotification(toastXml)
                        {
                            ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(3600)
                        };
            //toast.Activated += ToastActivated;
            //toast.Dismissed += ToastDismissed;
            //toast.Failed += ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(_applicationId).Show(toast);
            */
        }

        private void ValidateOsVersion()
        {
            var vs = Environment.OSVersion.Version;
            _showToast = vs.Major == 6 && vs.Minor >= 2 || vs.Major > 6;
        }

        /*
        private void ToastActivated(ToastNotification sender, object e)
        {
            Dispatcher.Invoke(() =>
            {
                Activate();
                Output.Text = "The user activated the toast.";
            });
        }

        private void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
        {
            String outputText = "";
            switch (e.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "The app hid the toast using ToastNotifier.Hide";
                    break;

                case ToastDismissalReason.UserCanceled:
                    outputText = "The user dismissed the toast";
                    break;

                case ToastDismissalReason.TimedOut:
                    outputText = "The toast has timed out";
                    break;
            }

            Dispatcher.Invoke(() => { Output.Text = outputText; });
        }

        private void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
        {
            Dispatcher.Invoke(() => { Output.Text = "The toast encountered an error."; });
        }
        */
    }
}