#pragma once
#ifndef __COMPLEX__
#define __COMPLEX__
#include <cmath>
#include <ostream>
#include <ccomplex>
class complex
{
	public:
	complex(double r = 0, double i = 0)
		:re(r), im(i)
	{
	}
	complex& operator +=(const complex&);
	double real() const { return re; }
	double imag() const { return im; }
	
	private:  
	double re, im;
};

 inline double imag (const complex& x)
{
	return x.imag();
}

 inline double real(const complex& x)
 {
	 return x.real();
 }

 inline complex operator + (const complex& x, const complex& y)
 {
	 return complex(real(x) + real(y), imag(x) + imag(y));
 }

 inline complex operator + (const double& x, const complex& y)
 {
	 return complex(real(x) + real(y), imag(x) + imag(y));
 }

 inline complex operator + (const complex& x, const double& y)
 {
	 return complex(real(x) + real(y), imag(x) + imag(y));
 }

	inline complex operator + (const complex& x)
	{
		return x;
	}

	inline complex operator - (const complex& x)
	{
		return complex(-real(x),-imag(x))  ;
	}

	inline complex operator == (const complex& x, const complex& y)
	{
		return real(x) == real(y)
			&& imag(x) == imag(y);
	}

	inline complex conj(const complex& x)
	{
		return complex(real(x), -imag(x));
	}


	std::ostream& operator << (std::ostream& os, const complex& x)
	{
		return os << '(' << real(x) << ',' << imag(x) << ')';
	}


#endif // !__COMPLEX__