using System;

Console.WriteLine("Enter a number to convert to binary (format as wholeNumber.Decimal): ");
string? testNumString = Console.ReadLine();
double testNumDub = double.Parse(testNumString);
int exponent = 0;

bool isNumNegative = false;
if (testNumDub < 0)
{
    isNumNegative = true;
}

if (isNumNegative)
{
    testNumDub = testNumDub * -1;
    testNumString = testNumDub.ToString();

    if(testNumDub % 1 == 0)
    {
        testNumString = testNumString + ".0";
    }
    Console.WriteLine(testNumString);
}

int indexOfDecimal = testNumString.IndexOf(".");

int tempWholeNumInput = Int32.Parse(testNumString.Substring(0, indexOfDecimal));
double tempDecimalInput = double.Parse(testNumString.Substring(indexOfDecimal));

Console.WriteLine(tempWholeNumInput);
Console.WriteLine(tempDecimalInput);

string convertedWholeNum = convertWholeNumToBinary(tempWholeNumInput);
string convertedDecimalNum = convertDecimalToBinary(tempDecimalInput);

Console.WriteLine("");
Console.WriteLine("The number converted to binary is: " + convertedWholeNum + "." + convertedDecimalNum);
Console.WriteLine("");

string joinedNum = convertedWholeNum + "." + convertedDecimalNum;

if (isNumNegative)
    joinedNum = joinedNum.Insert(0, "-");
else
    joinedNum = joinedNum.Insert(0, "+");

string normalizedNum = normalize(convertedWholeNum, convertedDecimalNum, joinedNum, ref exponent);
finalizeAndPrint(normalizedNum, exponent);


string convertWholeNumToBinary(int numToConvert)
{
    string convertedBinWholeNum = "";

    while (numToConvert != 0)
    {
        int remainder = numToConvert % 2;
        numToConvert = numToConvert / 2;

        convertedBinWholeNum = convertedBinWholeNum.Insert(0, remainder.ToString());
    }

    return convertedBinWholeNum;
}

string convertDecimalToBinary(double decimalNumToConvert)
{
    string convertedBinDecimal = "";
    int numOfDecimalPlaces = 0;
  

    while (decimalNumToConvert != 1.0 && numOfDecimalPlaces < 4)
    {
        numOfDecimalPlaces++;
        decimalNumToConvert = decimalNumToConvert - Math.Truncate(decimalNumToConvert);
        decimalNumToConvert = decimalNumToConvert * 2;

        convertedBinDecimal = convertedBinDecimal + Math.Truncate(decimalNumToConvert).ToString();
    }

    return convertedBinDecimal;
}

string normalize(string wholeNum, string decimalNum, string joinedNum, ref int exponent)
{
    int currentDecimalPlace;
    int desiredDecimalPlace;


    if (wholeNum == "")
    {
        currentDecimalPlace = joinedNum.IndexOf(".");
        desiredDecimalPlace = joinedNum.IndexOf("1") + 1;

        exponent =  currentDecimalPlace - desiredDecimalPlace + 1;
        // be sure to trim out leading zeros

        joinedNum = joinedNum.Insert(desiredDecimalPlace, ".");
        joinedNum = joinedNum.Remove(currentDecimalPlace, 1);

        //Trims out leading zeros
        char tempSign = joinedNum[0];
        joinedNum = joinedNum.Remove(0, 1);
        double tempJoinedNum = double.Parse(joinedNum);
        joinedNum = tempJoinedNum.ToString();
        joinedNum = joinedNum.Insert(0, tempSign.ToString());
    }

    else
    {
        currentDecimalPlace = joinedNum.IndexOf(".");
        desiredDecimalPlace = joinedNum.IndexOf("1") + 1;

        exponent = currentDecimalPlace - desiredDecimalPlace;

        joinedNum = joinedNum.Insert(desiredDecimalPlace, ".");
        joinedNum = joinedNum.Remove(currentDecimalPlace + 1, 1);
    }

    Console.WriteLine("Normalization");
    Console.WriteLine("The normalized binary number is:" + joinedNum + "*2^" + exponent);
    Console.WriteLine("");

    return joinedNum;
}

void finalizeAndPrint(string normalizedNum, int exponent)
{
    string sign;

    if (normalizedNum[0] == '-')
        sign = "1";
    else
        sign = "0";


    string significand;
    significand = normalizedNum.Substring(3);
    
    if(significand.Length > 4)
    {
        significand = significand.Substring(0, 4);
    }

    exponent = exponent + 3;
    string binaryExponent = convertWholeNumToBinary(exponent);

    //binaryExponent = binaryExponent.Substring(1);

    Console.WriteLine("The final binary representation in excess 3 is: ");
    Console.WriteLine("Sign: " + sign);
    Console.WriteLine("Exponent: " + binaryExponent);
    Console.WriteLine("Significand: " + significand);
}