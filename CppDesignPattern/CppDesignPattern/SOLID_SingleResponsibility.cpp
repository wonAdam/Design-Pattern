#define SRP
#ifdef SRP
#include <iostream>
#include <vector>
#include <fstream>


// Responsible for keep journal data
struct Journal
{
	std::string title;
	std::vector<std::string> entries;

	Journal(const std::string& title) : title(title) {}

	void add_entry(const std::string entry)
	{
		static int count = 1;
		entries.push_back(static_cast<std::string>(count++) + ": " + entry);
	}

	//// wrong
	//// seperation of concern
	//void save(const std::string& filename)
	//{
	//	std::ofstream ofs(filename);
	//	for (auto& e : entries)
	//		ofs << e << std::endl;
	//}
};

// Responsible for 
struct PersistenceManager
{
	void save(const Journal& j, const std::string& filename)
	{
		std::ofstream ofs(filename);
		for (auto& e : j.entries)
			ofs << e << std::endl;
	}
};


int main()
{
	Journal journal{ "Dear Diary" };
	journal.add_entry("I ate a bug");
	journal.add_entry("I cried today");

	PersistenceManager PM;
	PM.save(journal, "dirary.txt");

	std::cin.get();
	return;



}

#endif