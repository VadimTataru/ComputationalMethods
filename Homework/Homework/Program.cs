//Program starts here

using System.Text.RegularExpressions;

Console.WriteLine("Дан многочлен Ax^5 + 0,387x^4 + 1,4789x^3 + 1,0098x^2 + 1,222x + B " +
    "\nА так же коэффициенты A = 1,234 +- 0,001, B = -2,345 +- N * 10^(-4), " +
    "\n x = 0,234*N +- 3*10^(-3), где N - количество букв в Вашем ФИО");

List<double> resultKoefs = new List<double>();
List<double> resultErrors = new List<double>();

List<(double, double)> koeffs = new List<(double, double)> { (1.234, 0.001), (0.387, 0), (1.4789, 0), (1.0098, 0), (1.222, 0) };

(double, double) result = (0, 0);

Console.WriteLine();
Console.WriteLine("Введите N:");
int n = Convert.ToInt32(Console.ReadLine());
var x = (0.234 * n, 0.003);
var c = (0.987, n * 0.001);

koeffs.Add((-2.345, n * 0.0001));


getResultKoefGorner(koeffs, x.Item1);
getSumErrorGorner(koeffs, x.Item1);

Console.WriteLine($"Погрешность значения = {result.Item2}");
Console.WriteLine($"Результат: {result.Item1} +- {result.Item2}");

getRightNums((result));
SignificantNums(result.Item1);


getErrorAndCompair(koeffs, result);

resultKoefs.Clear();
getResultKoefGorner(koeffs, c.Item1);

Console.WriteLine("Коэффициенты многочлена при его делении на x - c: ");

for (int i = 0; i < resultKoefs.Count; i++)
{
    Console.Write(resultKoefs[i] + " ");
}

//Program ends here



void getSumErrorGorner(List<(double, double)> _errors, double _x)
{
    double sum = _errors[0].Item2;
    resultErrors.Add(sum);

    for (int i = 1; i < _errors.Count; i++)
    {
        sum = sum * Math.Abs(_x) + Math.Abs(resultKoefs[i - 1]) * _errors[i - 1].Item2;
        resultErrors.Add(sum);
    }

    result.Item2 = sum;
}

void getResultKoefGorner(List<(double, double)> _koef, double _x)
{
    double sum = _koef[0].Item1;
    resultKoefs.Add(sum);

    for (int i = 1; i < _koef.Count; i++)
    {
        sum = sum * _x + _koef[i].Item1;
        resultKoefs.Add(sum);
    }

    result.Item1 = sum;
}

void getErrorAndCompair(List<(double, double)> _errors, (double, double) resultGorner)
{
    double sum = _errors[0].Item2;
    for (int i = 1; i < _errors.Count; i++)
    {
        sum += _errors[i].Item2;
    }

    if (resultGorner.Item2 > sum)
        Console.WriteLine($"Погрешность по схеме Горнера: {resultGorner.Item2} > погрешность не по Горнеру {sum}");
    else
        Console.WriteLine($"Погрешность по схеме Горнера: {resultGorner.Item2} < погрешность не по Горнеру {sum}");
}

void getRightNums((double, double) num)
{
    string stringNum = num.Item1.ToString();
    byte[] numbers = new byte[stringNum.Length];
    int doteIndex = 0;
    List<byte> rightNums = new List<byte>();

    for (int i = 0; i < numbers.Length; i++)
    {
        if (Char.IsDigit(stringNum[i]))
        {
            numbers[i] = byte.Parse(stringNum[i].ToString());
        }
        else
            doteIndex = i;
    }

    for (int i = numbers.Length - 1; i >= 0; i--)
    {
        if (num.Item2 < (0.5 * Math.Pow(10, doteIndex - (i + 1))))
        {
            rightNums.Add(numbers[i]);
        }
    }
    rightNums.Reverse();


    Console.Write("Верные числа: ");
    printList(rightNums);
    Console.WriteLine();

}


void SignificantNums(double num)
{
    string numStr = num.ToString() + "0000";
    Regex reg = new Regex(@"0*$", RegexOptions.Multiline);
    string result = reg.Replace(numStr, "");
    result = result.Replace(",", "");
    char[] strs = result.ToCharArray();

    Console.WriteLine("Значимые числа: {0}", String.Join(' ', strs));

}

void printList(List<byte> list)
{
    for (int i = 0; i < list.Count; i++)
    {
        Console.Write(list[i] + " ");
    }
}