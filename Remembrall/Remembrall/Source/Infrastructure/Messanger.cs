using System;

namespace Remembrall.Source.Infrastructure
{
    public class Messanger
    {
        /// <summary>
        /// Синголтон для хранения подписок
        /// </summary>
        private static NotifyMessage _notify;

        public Messanger()
        {
            if (_notify == null)
                _notify = new NotifyMessage();
        }

        /// <summary>
        /// Добавление обработчиков события
        /// </summary>
        /// <param name="action"></param>
        public void AddUpdater(Action action)
        {
            _notify.UpdateViewNotify += new NotifyMessage.UpdateHolidayView(action);
        }

        /// <summary>
        /// Вызов обработчиков событий
        /// </summary>
        public void InvokeUpdate()
        {
            _notify?.InvokeToUpdateView();
        }

    }


    
}
