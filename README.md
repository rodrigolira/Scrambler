# Scrambler

![build](https://github.com/rodrigolira/Scrambler/workflows/build/badge.svg?branch=master) [![Scrambler-stable](https://img.shields.io/nuget/v/Scrambler?maxAge=3600&label=Scrambler%20nuget)](https://www.nuget.org/packages/Scrambler/)

Scrambler is an utility class to encode/decode an integer into/from an unrecognizable string. It is useful when you're working in a system where you hand an id of some sort to the end user and that id happens to be an integer autoincrement primary key of your relational database entity.

If you are working, say, on a helpdesk system and you tell the user he opened ticket `540` he'll be able to tell how many tickets you had so far and it opens the system up to easier brute force attacks. You could work with an encoding option like base64 but the format is well known and the encoded value is not readable for an end-user.

By default, instead of telling the user he opened ticket `540` you can say he opened ticket `KAI-1078671364`.

This code is based on the original PHP code by Richard Bentley on his [plugin for OSCommerce](https://apps.oscommerce.com/8VVWQ&scrambled-order-number).

## Installing

Scrambler is available as a Nuget package. You can install by running:

`PM> Install-Package Scrambler`

or through the CLI:

`> dotnet add package Scrambler`

## Using

To use Scrambler, simply create a new instance of the class and use the encode and decode methods.

````csharp
Scrambler scrambler = new Scrambler();

int ticketNumber = 540;

// to encode
string encodedValue = scrambler.Encode(ticketNumber);

// to decode
int originalTicketNumber = scrambler.Decode(encodedValue);
````

## Configuring

Scrambler accepts an object of type `ScramblerOptions` in its constructor. These options controls how the encoded values are generated. They should be unique to your site. When an instance of `ScramblerOptions` is not given to Scrambler, it will use a default options instance.

Because of the way the encoding algorithm works, these options have a determined set of valid values as the table below shows:

| Parameter        | Min Value      | Max Value      |
| ---------------- | -------------- | -------------- |
| Xor1             | 0              | Int32.MaxValue |
| Shift1           | 1              | 31             |
| Xor2             | 0              | Int32.MaxValue |
| Shift2           | 1              | 31             |
| Xor3             | 0              | Int32.MaxValue |
| Shift3           | 1              | 31             |
| HashXor          | Int32.MinValue | Int32.MaxValue |
| HashShift        | 1              | 31             |
| HashPrefixLength | 1              | N/A            |

Here's an example of creating an instance of Scrambler providing custom options:

````csharp
Scrambler scrambler = new Scrambler(new ScramblerOptions(
    xor1: 0,
    shift1: 7,
    xor2: 0x58230a67,
    shift2: 23,
    xor3: 0x71eba836,
    shift3: 13,
    hashXor: Int32.MinValue,
    hashShift: 9,
    hashPrefixLength: 3
));
````
