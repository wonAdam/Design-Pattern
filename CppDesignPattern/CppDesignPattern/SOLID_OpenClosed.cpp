//#define OCP
#ifdef OCP
#include <iostream>
#include <vector>
#include <fstream>

enum class Color { red, green, blue }; 
enum class Size {small, medium, large};

struct Product {
	std::string name;
	Color color;
	Size size;
};

//// wrong
//struct ProductFilter
//{
//	std::vector<Product*> by_color(std::vector<Product*> items, Color color)
//	{
//		std::vector<Product*> result;
//		for (auto& i : items)
//		{
//			if (i->color == color)
//				result.push_back(i);
//		}
//		return result;
//	}
//
//	std::vector<Product*> by_color_and_size(std::vector<Product*> items, Color color, Size size)
//	{
//		std::vector<Product*> result;
//		for (auto& i : items)
//		{
//			if (i->color == color && i->size == size)
//				result.push_back(i);
//		}
//		return result;
//	}
//};

//struct ProductFilter
//{
//	std::vector<Product*> by_color(std::vector<Product*> items, Color color)
//	{
//		std::vector<Product*> result;
//		for (auto& i : items)
//		{
//			if (i->color == color)
//				result.push_back(i);
//		}
//		return result;
//	}
//};


template <typename T>
struct Specification
{
	virtual bool is_satisfied(T* item) = 0;
};

template <typename T>
struct Filter
{
	virtual std::vector<T*> filter(std::vector<T*> items, Specification<T>& spec) = 0;
};

template <typename  T>
struct AndSpecification : Specification<T>
{
	Specification<T>& first;
	Specification<T>& second;

	AndSpecification(Specification<T>& first, Specification<T>& second)
		: first(first), second(second) {}

	bool is_satisfied(Product* item) override {
		return first.is_satisfied(item) && second.is_satisfied(item);
	}
};

template<typename T> 
AndSpecification<T>& operator&&(const Specification<T>& first, const Specification<T>& second)
{
	return { first, second };
}



struct BetterFilter : Filter<Product>
{
	std::vector<Product*> filter(std::vector<Product*> items, Specification<Product>& spec) override
	{
		std::vector<Product*> result;
		for (auto& item : items)
			if (spec.is_satisfied(item))
				result.push_back(item);

		return result;
	}
};

struct ColorSpecification : Specification<Product>
{
	Color color;

	ColorSpecification(Color color) : color(color) {}

	bool is_satisfied(Product* item) override {
		return item->color == color;
	}
};

struct SizeSpecification : Specification<Product>
{
	Size size;

	SizeSpecification(Size size) : size(size) {}

	bool is_satisfied(Product* item) override {
		return item->size == size;
	}
};


int main()
{
	Product apple{ "Apple", Color::green, Size::small };
	Product tree{ "Tree", Color::green, Size::large};
	Product house{ "House", Color::blue, Size::large };

	std::vector<Product*> items{ &apple, &tree, &house };

	/*ProductFilter pf;
	const std::vector<Product*>& green_thing = pf.by_color(items, Color::green);

	for (auto& g : green_thing)
	{
		std::cout << g->name << std::endl;
	}*/

	BetterFilter bf;
	ColorSpecification green(Color::green);
	for (auto& item : bf.filter(items, green))
	{
		std::cout << item->name << " " <<  "green"  << std::endl;
	}

	AndSpecification<Product> green_and_large = ColorSpecification(Color::green) && SizeSpecification(Size::large);

	std::cin.get();

	return 0;
}

#endif