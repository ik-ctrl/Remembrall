namespace Remembrall.Source.Infrastructure
{
    public class NotifyMessage
    {
        /// <summary>
        /// Делегат для привязки методов
        /// </summary>
        public delegate void UpdateHolidayView();

        /// <summary>
        /// События для оповещения подписчиков
        /// </summary>
        public event UpdateHolidayView UpdateViewNotify;

        /// <summary>
        /// Вызов обработчиков для обновления формы
        /// </summary>
        public void InvokeToUpdateView()
        {
            UpdateViewNotify?.Invoke();
        }
    }
}
