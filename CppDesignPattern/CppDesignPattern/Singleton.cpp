#define Singleton
#ifdef Singleton

#include <iostream>
#include <vector>
#include <string>
#include <map>
#include <fstream>

class SingletonDatabase
{
private:
	SingletonDatabase()
	{
		std::cout << "Initializing database\n";
		std::ifstream ifs("capitals.txt");
		std::string s, s2;

		while (getline(ifs, s))
		{
			getline(ifs, s2);
			int pop = std::stoi(s2);
			capitals[s] = pop;
		}
	}
	std::map<std::string, int> capitals;

public:
	SingletonDatabase(SingletonDatabase& rhs) = delete;
	void operator=(const SingletonDatabase& rhs) = delete;

	static SingletonDatabase& get()
	{
		static SingletonDatabase db;
		return db;
	}

	int get_population(const std::string& name)
	{
		return capitals[name];
	}
};

int main()
{
	int tokyo_pop = SingletonDatabase::get().get_population("Tokyo");
	std::cout << tokyo_pop << std::endl;
}

#endif