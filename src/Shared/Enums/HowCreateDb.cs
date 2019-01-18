namespace Shared.Kernel.Enums
{
    public enum HowCreateDb
    {
        None,                 //Не создавать
        Migrate,              //С помощью последней миграции 
        EnsureCreated         //Принудительно
    }
}