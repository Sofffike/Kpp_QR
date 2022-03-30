using System;

namespace Upload_File_MVC_Core.Models
{
    public class Persons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
       // public DateTime Date { get; set; }   // Дата рождения 
        public int DocSeries { get; set; }
        public int DocNumber { get; set; }
        public string DocWhom { get; set; }  //кем выдан паспорт


        public string FileName { get; set; } // ссылка на связанную модель для получения фото
     //   public File FileModel { get; set; }


       // public string Activity { get; set; }  // активность пропуска


        /// создать вью где будет дата трудоустройства, вакансия 

    }
}
