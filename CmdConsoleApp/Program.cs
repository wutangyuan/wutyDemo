// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

if (args.Length <= 0)
{
    Console.WriteLine($"没有输入参数");
    return;
}
Console.WriteLine($"输入参数个数: {args.Length}");
foreach (var arg in args)
{
    Console.WriteLine($"参数为: {arg}");
    Console.WriteLine($"参数的长度为：{arg.Length}");
}


