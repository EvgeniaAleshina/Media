namespace Малахов.Classes
{
    class Data
    {
        public static int Access { get; set; }
        public static int UserID { get; set; }
        public static bool IsManager() => Access == 0;
        public static bool IsAdmin() => Access == 1;
    }
}
