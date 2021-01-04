//#define LSP
#ifdef LSP
#include <iostream>
#include <vector>
#include <fstream>

class Rectangle
{
protected:
	int width, height;
public:
	Rectangle(int width, int height) : width(width), height(height) {}

	int getWidth()
	{
		return width;
	}
	virtual void setWidth(int width) 
	{
		this->width = width;
	}

	int getHeight() 
	{
		return height;
	}

	virtual void setHeight(int height)
	{
		this->height = height;
	}

	int area() const { return width * height; }
};

class Square : public Rectangle
{
public:
	Square(int size) : Rectangle(size, size) {}
	void setWidth(int size) override
	{
		this->height = this->width = size;
	}

	void setHeight(int size) override
	{
		this->height = this->width = size;
	}
};

void process(Rectangle& r)
{
	int w = r.getWidth();
	r.setWidth(10);

	std::cout << "expected area = " << (w * 10)
		<< ", got " << r.area() << std::endl;
}

int main()
{
	Rectangle r{ 3, 4 };
	process(r);

	// height width 둘다 10이 되어버림.
	Square sq{ 5 };
	process(sq);
}


#endif