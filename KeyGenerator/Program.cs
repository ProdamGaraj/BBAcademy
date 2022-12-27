// See https://aka.ms/new-console-template for more information
using KeyGenerator.DataGenerator;

int j = int.Parse(args[0]);
new DataGenerator();
DataGenerator dg = DataGenerator.instance;
for (int i = 0; i < j; i++)
{
    dg.CreateNewKey();
}