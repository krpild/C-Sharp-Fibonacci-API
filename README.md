# C-Sharp-Fibonacci-API

Author: Kristjan Pilden

An ASP.NET Core Web API that generates a subsequence of Fibonacci numbers between a start and end index. Includes optional caching, memory and time limits, and async processing.

## How to use:
The program has a single endpoint:

### ``/api``

It takes in both ``POST`` and ``GET`` requests.

The cache TTL can be changed in ```appsettings.json``` under ```CacheSettings``` in ```CacheTtl```. TTL is in minutes.

For ease of use go to [your localhost]/swagger.

Program supports PostMan too.

## ``GET`` request

The get request accepts query parameters such as:

- Start (required) : int    ``[The starting index of the Fibonacci sequence]``
- End (required) : int   ``[The end index of the Fibonacci sequence]``
- Cache : bool   ```[Whether the program is allowed to use cache to cut down on calculation]```
- MaxTime : int   ```[How many milliseconds are permitted for the program]```
- MaxMemory : int   ```[How much memory is permitted for the program]```

The limit for start and end indexes is 20000. The program will check if the values are in a permitted range. It can be changed in ```Util.cs``` in the method ```IsValidFibonacciIndex()```.

## ``POST`` request

The post request contains all the same requirements. Here's an example request:

```
{
  "start": 0,
  "end": 1000,
  "cache": true,
  "maxTime": 0,
  "maxMemory": 0
}
```

## Response

Upon doing either a POST or GET command with variables ```start = 0``` and ```end = 10```, the program should return a JSON that looks similar to this:
```
{
  "sequence": [
    "0",
    "1",
    "1",
    "2",
    "3",
    "5",
    "8",
    "13",
    "21",
    "34",
    "55"
  ],
  "errors": null
}
```

Due to the Fibonacci algorithm using BigInteger during calculations, the numbers in the sequence are strings, as an index that is higher than 93 will result in long overflow. To combat this problem, BigInteger has been employed to solve the issue.

## Architecture

This was my first ever C# API, so the architecture may be a little bit off. I tried to adhere to the single responsibility principle and logically group or separate things as much as possible. Unfortunately in ```FibonacciServices.cs``` due to many checks required, the method ```GetFibonacciRange()``` got a bit bloated.

The overall architecture is separated into three major bits:
- API ``Controllers and the web application.``
- Core ``Core logic of the application.``
- Test ``A small testing environment to make sure the algorithm is working as intended.``

I separated the Web critical logic from the base logic of the Fibonacci algorithm, so if the need would arise, I could test or expand on just the Fibonacci algorithm implementation.

## Fibonacci algorithm

The Fibonacci algorithm used in this program is the matrix exponentiation algorithm. Its implementation is in ``Core/Fibonacci.cs``.

The Fibonacci sequence is notorious for being slow and it's often implemented via recursion or dynamic programming. The problem required me to implement an algorithm that would take a SUBsequence of the Fibonacci sequence, meaning, it could start at 10000. When using naive solutions, the computer has to do tons of redundant work, increasing wait time and ultimately making the client wait. That is obviously undesirable.

The Matrix exponentiation algorithm can simply start at index 10000 due to the following definition:

```math
\begin{pmatrix}
1 & 1 \\
1 & 0 \\
\end{pmatrix}^{n}= \begin{pmatrix}
F_{n+1} & F_{n} \\
F_{n} & F_{n-1} \\
\end{pmatrix}
```

Although there are 3 redundant terms on the right side, due to the nature of the matrix calculation being relatively light on computation, the end result is leagues ahead of dynamic and recursive methods.

The implementation was inspired by Oran Looney's implementation in Python (https://www.oranlooney.com/post/fibonacci/)

Further reading at https://en.wikipedia.org/wiki/Fibonacci_sequence#Matrix_form is encouraged, as it's a really interesting solution.

### Unimportant bit about personal life

Due to already being in the middle of an internship, working through a couple university courses and writing the final thesis, this program took way longer to implement than expected. For most of the work week I was busy and about 80% of the work was done on Sunday. This was my first C# API. I've had previous experience with writing Razor pages and writing APIs in Java Spring and Python, but this is a new territory for me.

Hopefully assessment was not disappointing and showed off my approach to problem solving reasonably well. I am open to feedback regardless whether I am accepted or not. Learning never stops.

Thank You for reading!
