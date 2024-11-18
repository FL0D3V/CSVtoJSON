namespace CSVtoJSON_Converter.Helpers
{
    public static class Messages
    {
        public const string WrongUsage = "Wrong Usage!\n" +
                                          " - Use like: CSVtoJSON FILENAME NUMBER_OF_ITEMS\n" +
                                          " - Eg.: CSVtoJSON {/LOCATION/FILENAME}.csv {HEADING}";
        public const string Loaded = "Successfully loaded the file!";
        public const string NowConverting = "File will now get converted!";
        public const string EnterFilePath = "Enter the filepath: ";
        public const string EnterElementCount = "Enter the number of elements: ";
        public const string EnterHeadingKey = "Enter the heading key: ";
        public const string EnterExtraItems = "Enter extra items: ";
        public const string SuccessfullyConverted = "File successfully converted!";
        public const string Closing = "Press any key to close...";
        public const string NoEntryFound = "This entry was not found!";
        public const string FileNotFound = "File not found!";
        public const string NoArgs = "Starting with no args.";
        public const string Args = "Starting with args.";
    }
}
