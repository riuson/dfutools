namespace DfuConvCli {
    public static class Program {
        private static void Main(string[] args) {
            using (var proc = new Processor()) {
                proc.Process(args);
            }
        }
    }
}
