namespace DfuToolCli {
    public static class Program {
        private static void Main(string[] args) {
            using (var proc = new CommandsProcessor()) {
                proc.Process(args);
            }
        }
    }
}
