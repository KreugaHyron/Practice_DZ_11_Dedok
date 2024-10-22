using System.Text;
using System.Text.RegularExpressions;
//1
Console.WriteLine("Task_1: ");
Random random = new Random();
int[] numbers = new int[100];
for (int i = 0; i < 100; i++)
{
    numbers[i] = random.Next(1, 1000);
}

List<int> primeNumbers = numbers.Where(IsPrime).ToList();

List<int> fibonacciNumbers = numbers.Where(IsFibonacci).ToList();

File.WriteAllLines("PrimeNumbers.txt", primeNumbers.Select(n => n.ToString()));

File.WriteAllLines("FibonacciNumbers.txt", fibonacciNumbers.Select(n => n.ToString()));

string stats = $"Generated Numbers: {string.Join(", ", numbers)}\n" +
               $"Prime Numbers: {string.Join(", ", primeNumbers)}\n" +
               $"Fibonacci Numbers: {string.Join(", ", fibonacciNumbers)}\n" +
               $"Total Primes: {primeNumbers.Count}\n" +
               $"Total Fibonacci: {fibonacciNumbers.Count}\n";

Console.WriteLine(stats);

Regex regex = new Regex(@"\d+");
var matches = regex.Matches(stats);

Console.WriteLine($"\nStatistics (using Regex): Found {matches.Count} numbers in the output.");
    static bool IsPrime(int number)
{
    if (number < 2) return false;
    for (int i = 2; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0) return false;
    }
    return true;
}

static bool IsFibonacci(int number)
{
    int a = 0;
    int b = 1;
    while (b < number)
    {
        int temp = b;
        b = a + b;
        a = temp;
    }
    return b == number || number == 0;
}
Console.WriteLine();
//2
Console.WriteLine("Task_2: ");
Console.Write("One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed in his bed into a horrible vermin. He lay on his armour-like back, and if he lifted his head a little he could see his brown belly, slightly domed and divided by arches into stiff sections.");
Console.WriteLine();
Console.Write("Введіть слово для пошуку: ");
string searchWord = Console.ReadLine();

Console.Write("Введіть слово для заміни: ");
string replaceWord = Console.ReadLine();
string filePath = "text.txt";
string text = File.ReadAllText(filePath);

Regex regex_1 = new Regex(Regex.Escape(searchWord));
int count = regex_1.Matches(text).Count;  

if (count > 0)
{
    string replacedText = regex_1.Replace(text, replaceWord);

    File.WriteAllText(filePath, replacedText);

    Console.WriteLine($"Кількість знайдених і замінених слів: {count}");
    Console.WriteLine("Текст успішно оновлено.");
}
else
{
    Console.WriteLine($"Слово '{searchWord}' не знайдено в тексті.");
}
Console.WriteLine();
//3
Console.WriteLine("Task_3: ");
Console.Write("Введіть шлях до файлу з текстом: ");
string textFilePath = Console.ReadLine();

Console.Write("Введіть шлях до файлу зі словами для модерації: ");
string moderationFilePath = Console.ReadLine();

try
{
    string textContent = File.ReadAllText(textFilePath, Encoding.UTF8);
    string[] moderationWords = File.ReadAllLines(moderationFilePath, Encoding.UTF8);

    foreach (string word in moderationWords)
    {
        if (!string.IsNullOrWhiteSpace(word))
        {
            string pattern = $@"\b{Regex.Escape(word)}\b"; 
            string replacement = new string('*', word.Length);
            textContent = Regex.Replace(textContent, pattern, replacement, RegexOptions.IgnoreCase);
        }
    }

    File.WriteAllText(textFilePath, textContent, Encoding.UTF8);

    Console.WriteLine("Модерація завершена. Результат збережено у вихідний файл.");
}
catch (Exception ex)
{
    Console.WriteLine($"Сталася помилка: {ex.Message}");
}
Console.WriteLine();
//4
Console.WriteLine("Task_4: ");
Console.WriteLine("Введіть шлях до папки:");
string folderPath = Console.ReadLine();

Console.WriteLine("Введіть маску файлів для пошуку (наприклад, *.txt):");
string searchPattern = Console.ReadLine();

Console.WriteLine("Оберіть дію: 1 - Показати файли, 2 - Видалити файли");
int action = int.Parse(Console.ReadLine());
try
{
    var files = Directory.GetFiles(folderPath, searchPattern, SearchOption.AllDirectories);

    if (files.Length == 0)
    {
        Console.WriteLine("Файли за вказаною маскою не знайдені.");
        return;
    }

    if (action == 1)
    {
        Console.WriteLine("Знайдені файли:");
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }
    else if (action == 2)
    {
        foreach (var file in files)
        {
            File.Delete(file);
            Console.WriteLine($"Файл видалено: {file}");
        }
    }
    else
    {
        Console.WriteLine("Невідома дія.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Сталася помилка: {ex.Message}");
}
Console.WriteLine();
//5
Console.WriteLine("Task_5: ");
string inputFile = "numbers.txt";

string positiveFile = "positive_numbers.txt";
string negativeFile = "negative_numbers.txt";
string twoDigitFile = "two_digit_numbers.txt";
string fiveDigitFile = "five_digit_numbers.txt";

int positiveCount = 0;
int negativeCount = 0;
int twoDigitCount = 0;
int fiveDigitCount = 0;

List<int> positiveNumbers = new List<int>();
List<int> negativeNumbers = new List<int>();
List<int> twoDigitNumbers = new List<int>();
List<int> fiveDigitNumbers = new List<int>();

try
{
    using (StreamReader reader = new StreamReader(inputFile))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            int number;
            if (int.TryParse(line, out number))
            {
                if (number > 0)
                {
                    positiveCount++;
                    positiveNumbers.Add(number);
                }
                else if (number < 0)
                {
                    negativeCount++;
                    negativeNumbers.Add(number);
                }

                if (Math.Abs(number) >= 10 && Math.Abs(number) < 100)
                {
                    twoDigitCount++;
                    twoDigitNumbers.Add(number);
                }

                if (Math.Abs(number) >= 10000 && Math.Abs(number) < 100000)
                {
                    fiveDigitCount++;
                    fiveDigitNumbers.Add(number);
                }
            }
        }
    }
    Console.WriteLine($"Кількість додатних чисел: {positiveCount}");
    Console.WriteLine($"Кількість від'ємних чисел: {negativeCount}");
    Console.WriteLine($"Кількість двозначних чисел: {twoDigitCount}");
    Console.WriteLine($"Кількість п'ятизначних чисел: {fiveDigitCount}");

    File.WriteAllLines(positiveFile, positiveNumbers.ConvertAll(x => x.ToString()));
    File.WriteAllLines(negativeFile, negativeNumbers.ConvertAll(x => x.ToString()));
    File.WriteAllLines(twoDigitFile, twoDigitNumbers.ConvertAll(x => x.ToString()));
    File.WriteAllLines(fiveDigitFile, fiveDigitNumbers.ConvertAll(x => x.ToString()));

    Console.WriteLine("Файли з числами створено успішно.");
}
catch (Exception ex)
{
    Console.WriteLine($"Сталася помилка: {ex.Message}");
}