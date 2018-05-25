using ImageService.Communication.Modal;

namespace ImageService.Communication
{
    interface ITCPServerChannel
    {
        void Start();

        void Stop();

        void SendLog(object sender, MessageReceivedEventArgs e);
    }
}
