using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInput
{
    public class ModelGame
    {
        /// <summary>
        /// Уровень игры.
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// Текст для игры
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Время таймер
        /// </summary>
        public int TimeTick { get; set; }
        /// <summary>
        /// Номер текста
        /// </summary>
        public int NumberText { get; set; }
    }
}
