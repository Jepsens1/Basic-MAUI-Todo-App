namespace ToDoApp
{
    /// <summary>
    /// Represents constant values needed for sqlite database
    /// </summary>
    public static class Constants
    {
        public const string DatabaseFilename = "todolist.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
