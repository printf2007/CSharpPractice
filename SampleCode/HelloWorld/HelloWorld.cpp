#include <iostream>
#include "complex.h"

int main()
{
    complex c1(2, 1);
    complex c2(3,4);
    complex c3();
    std::cout << "Hello World!\n";
    std::cout << c1.real();
    std::cout << imag(c1);
    std::cout << c1<<conj(c1);
}


