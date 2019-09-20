#include <iostream>
#include <stdlib.h>
#include <set>

using namespace std;

// Size of arrays
const int arr_size = 20;

// Struct holding our arrays
struct nums
{
	// References the 'starting point' of the blocks
	char *loc[arr_size];

	// Stores an array of sizes for the blocks
	int values[arr_size];
};

/* Equation for recursive definition */
void eq(int intArr[], int index)
{
	if (index == 20)
	{
		return;
	}

	if (index == 0)
	{
		intArr[index] = 2900;
	}
	else
	{
		intArr[index] = intArr[index - 1] * 2;
	}
	index++;
	eq(intArr, index);
}

/* Create the 'blocks' of memory and assigns them random upper-case char values */
void createMem(char* memLocs[], int sizes[])
{
	for (int i = 0; i < arr_size; i++)
	{
		memLocs[i] = new char[sizes[i]];

		// Fills array with random chars

		for (int k = 0; k < sizes[i]; k++)
		{
			memLocs[i][k] = (rand() % 26) + 'A';
		}
	}
}

/* Print first 10 values of a block */
void printMem(char* memLocs[], int accessptr)
{
	for (int i = 0; i < 10; i++)
	{
		cout << memLocs[accessptr][i] << endl;
	}
}

/* Debug Printing */
void debugPrinting(char* memLocs[], int sizes[])
{
	for (int i = 0; i < arr_size; i++)
	{
		cout << sizes[i] << endl;
	}

	for (int i = 0; i < arr_size; i++)
	{
		for (int k = 0; k < 10; k++)
		{
			cout << memLocs[i][k] << endl;
		}
	}
}

/* Deallocate all function */
void deallocAll(char* memLocs[], set<int> &deallocList)
{
	for (int i = 0; i < arr_size; i++)
	{
		delete memLocs[i];
		memLocs[i] = nullptr;
		deallocList.insert(i);
		cout << "finished block # " << i << endl;
	}
}

/* Menu */
int menu(char* memLocs[], int sizes[])
{
	// Construct iterator for navigating set
	set<int>::iterator itr;

	// Create set for handling unique elements in a collection
	set<int> deallocList;

	int menuChoice = 0;

	// Main Menu
	while (menuChoice != 4)
	{
		cout << "Memory Manager" << endl;
		cout << "1. Access a Pointer" << endl;
		cout << "2. List deallocated Memory" << endl;
		cout << "3. Deallocate all Memory" << endl;
		cout << "4. Exit" << endl;

		cin >> menuChoice;

		int subMenuChoice;

		// Case 1, Submenu once pointer has been selected
		switch (menuChoice)
		{
		case 1:
			int accessptr;

			subMenuChoice = 0;

			// Access a 'block' of memory
			cout << "Which block would you like to access?" << endl;
			cin >> accessptr;

			while (subMenuChoice != 3) {
				cout << "What do you want to do? MemBlock(#" << accessptr << ")" << endl;
				cout << "1. Print first 10 cells" << endl;
				cout << "2. Delete Memory" << endl;
				cout << "3. Return to Main Menu" << endl;

				cin >> subMenuChoice;

				switch (subMenuChoice)
				{
					// Case 1, Print first 10 items
				case 1:
					if (memLocs[accessptr] == NULL)
					{
						memLocs[accessptr] = new char[sizes[accessptr]];

						for (int i = 0; i < sizes[accessptr]; i++)
						{
							memLocs[accessptr][i] = (rand() % 26) + 'A';
						}

						printMem(memLocs, accessptr);

						deallocList.erase(accessptr);
					}
					else
					{
						printMem(memLocs, accessptr);
					}
					break;

					// Case 2, delete the currently selected block
				case 2:
					delete memLocs[accessptr];
					memLocs[accessptr] = NULL;
					deallocList.insert(accessptr);
					cout << "Memory Block #" << accessptr << " Erased!" << endl;
					break;

					// Return to main menu
				case 3:
					break;

				default:
					break;
				}
			}
			break;

			// Case 2, Lists all deallocated memory
		case 2:
			for (itr = deallocList.begin(); itr != deallocList.end(); itr++)
			{
				cout << (*itr) << " ";
			}
			cout << endl;
			break;

			// Case 3, Deallocates all memory
		case 3:
			deallocAll(memLocs, deallocList);
			break;

			// Case 4, Exits program and deallocates all memory upon closing (manually)
		case 4:
			deallocAll(memLocs, deallocList);
			return 0;
		default:
			break;
		}
	}
}

/* Main */
int main()
{
	// Initialize our Struct
	nums struc;

	// Recursive definition
	eq(struc.values, 0);

	// creates and fills memory
	createMem(struc.loc, struc.values);

	// Debugging
	//debugPrinting(struc.loc, struc.values);

	// Menu
	menu(struc.loc, struc.values);

	return 0;
}





