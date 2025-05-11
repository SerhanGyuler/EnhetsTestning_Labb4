namespace EnhetsTestning_Labb4
{
    class Program
    {
        static void Main(string[] args)
        {
            LibrarySystem library = new LibrarySystem();
            UserInterface.DisplayMenu(library);
        }
    }
}
