// https://adventofcode.com/2023/day/12

var report = Read(line =>
{
    var parts = line.Split(' ');
    return new ConditionRecord(parts[0], parts[1].Split(',').Select(int.Parse).ToArray());
});

report.First().Springs.ToConsole();
report.First().Damages.ToConsole();

class ConditionRecord
{
    public string Springs { get; set; }

    public int[] Damages { get; set; }

    public ConditionRecord(string springs, int[] damages)
    {
        Springs = springs;
        Damages = damages;
    }

    public int Count()
    {

        return 0;
    }
}