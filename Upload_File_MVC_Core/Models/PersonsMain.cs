namespace Upload_File_MVC_Core.Models
{
    public class PersonsMain
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        // public DateTime Date { get; set; }   // Дата рождения 
        public int DocSeries { get; set; }
        public int DocNumber { get; set; }
        public string DocWhom { get; set; }  //кем выдан паспорт
        public string FilName { get; set; }
    }
}
