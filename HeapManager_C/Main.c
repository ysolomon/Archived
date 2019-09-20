#include <stdio.h>
#include <math.h>

/* Block data that will be used in determining how to manage our memory
   will act in a singly linked-list structure. */
struct Block
{
	int block_size; // # of bytes in each section
	struct Block *next_block; // block type
};

// The size of the overhead inside our structure
const int OVERHEAD_SIZE = sizeof(struct Block);
// The size of a void pointer
const int VOID_PTR_SIZE = sizeof(void*);
// Global ptr that will always point to first free block in mem
struct Block *FREE_HEAD;

// declare functions, implementations below main
void my_initialize_heap(int);
void* my_alloc(int);
void my_free(void*);
void calculateStdDev();

int main()
{	
	// Test Cases for initialization
	//my_initialize_heap(1000);
	
	calculateStdDev(1000);

	// ask for a positive int n
	// allocate space for an array of n ints
	// read n ints from standard in into the array allocated
	// calculate and print the std dev of ints entered using formula

	// Test cases for heap allocation

	// 1
	/*struct Block *one = my_alloc(20);
	printf("Address of block is at %p\n", one);
	my_free(one);
	struct Block *two = my_alloc(20);
	printf("Address of block is at %p\n", two);*/

	// 2
	/*struct Block *one = my_alloc(sizeof(int));
	struct Block *two = my_alloc(sizeof(int));
	printf("Address of block is at %p\n", one);
	printf("Address of block is at %p\n", two);*/

	// 3
	/*struct Block *one = my_alloc(sizeof(int));
	struct Block *two = my_alloc(sizeof(int));
	printf("Address of block is at %p\n", two);
	struct Block *three = my_alloc(sizeof(int));
	my_free(two);
	struct Block *four = my_alloc(sizeof(double));
	printf("Address of block is at %p\n", four);
	struct Block *five = my_alloc(sizeof(int));
	printf("Address of block is at %p\n", five);*/

	// 4
	/*struct Block *one = my_alloc(sizeof(char));
	struct Block *two = my_alloc(sizeof(int));
	printf("Address of block is at %p\n", one);
	printf("Address of block is at %p\n", two);*/

	// 5
	/*struct Block *two = my_alloc(sizeof(int));
	struct Block *one = my_alloc(sizeof(int[100]));
	//struct Block *two = my_alloc(sizeof(int));
	printf("Address of block is at %p\n", one);
	printf("Address of block is at %p\n", two);
	my_free(one);
	printf("Address of block is at %p\n", two);*/

	// hold console up
	//getchar();

	return 0;
}

void my_initialize_heap(int size)
{
	// will point to to the heap
	FREE_HEAD = malloc(size);
	// assigns heap size
	FREE_HEAD->block_size = size - OVERHEAD_SIZE;
	// no nodes in list
	FREE_HEAD->next_block = NULL;

	printf("Created a heap of %d bytes\n", (size - OVERHEAD_SIZE));
	return;
}
 
// size can be any positive int, any block must be a multiple of void* size
void *my_alloc(int size)
{
	// block pointer to point to FREE_HEAD's original position, first free block
	struct Block *current_block = FREE_HEAD;

	// block pointer to previous node, for non-split allocations
	struct Block *prev_block = current_block;

	// TODO round up the result of data mod void ptr size
	if (size % VOID_PTR_SIZE != 0)
	{
		size = size + (VOID_PTR_SIZE - (size % VOID_PTR_SIZE));
		printf("We've rounded up to %d\n", size);
	}

	// look for a block with a large enough size to fit
	while (current_block != NULL)
	{
		printf("Current free block is %d, bytes\n", current_block->block_size);

		// we have room
		if (current_block->block_size >= size)
		{
			// determine when to split ADD FROM THE END
			// if the difference between the block size and the size of memory
			// is less than overhead (8) and the minimum size for an block allocation (4)
			if ((current_block->block_size - size) >= (OVERHEAD_SIZE + VOID_PTR_SIZE))
			{
				// find the difference
				int next_addr = (current_block->block_size + OVERHEAD_SIZE) - (size + OVERHEAD_SIZE);
				// when splitting update current free block's size, JUST VALUES
				current_block->block_size -= size + OVERHEAD_SIZE;
				// move the pointer to the location of the next block
				current_block = (struct Block*) ((char*) current_block + next_addr);
				// initialize the block after moving
				current_block->block_size = size;
				current_block->next_block = NULL;

				printf("Split allocation of %d, bytes of memory\n\n", size);
			}
			// non-split allocation, it fits just won't split, fragmentation
			else
			{
				// set previous to the current's next
				prev_block->next_block = current_block->next_block;

				// in the case that we immediately fill our heap
				if (prev_block == current_block)
				{
					// no more free spaces
					FREE_HEAD = NULL;
				}
				else
				{
					// set current to null
					current_block->next_block = NULL;
				}
				printf("Non-split allocation of %d, bytes of memory\n\n", size);
			}

			// found our block & update free head
			return current_block + OVERHEAD_SIZE;
		}

		// we don't have room, store previous node for first time through
		if (prev_block != current_block)
		{
			prev_block = prev_block->next_block;
		}
		// move to next block
		current_block = current_block->next_block;
	}
	// otherwise, no room
	printf("No available Memory\n");
	return 0;
}

void my_free(void *data)
{
	// manipulate a pointer ptr, cast to type block
	struct Block* ptr = data;

	// move back a size of 8
	ptr -= OVERHEAD_SIZE;

	// select will point to FREE_HEAD
	ptr->next_block  = FREE_HEAD;

	// FREE_HEAD will point to new head
	FREE_HEAD = ptr;

	printf("%d bytes of data freed\n", ptr->block_size);
}


void calculateStdDev()
{
	// serves to be as int n
	int elements;
	// block pointer that will point to our heap elements
	int *heap_ptr;
	// hold the mean
	float mean = 0;
	// hold the variance
	float variance = 0;
	// hold the std dev
	float stdev = 0;

	my_initialize_heap(1000);

	printf("How many elements do you want?");
	scanf("%d", &elements);
	// create an array of n # of elements, starts at data
	heap_ptr = my_alloc(sizeof(int) * elements);

	// TODO enter elements 
	for (int i = 0; i < elements; i++)
	{
		int num;
		printf("Enter a number, %d more left:\n", elements - i);
		scanf("%d", &num);
		// store the number by hand, can't use block pointers
		*(heap_ptr + (sizeof(int) * i)) = num;
		// calculate mean
		mean += *(heap_ptr + (sizeof(int) * i));
	}
	// determine mean
	mean = mean / elements;
	printf("Mean is: %0.2f\n", mean);

	 //print back
	for (int i = 0; i < elements; i++)
	{
		printf("%d\n", *(heap_ptr + (sizeof(int) * i)));
	}

	for (int i = 0; i < elements; i++)
	{
		variance += pow((*(heap_ptr + sizeof(int) * i) - mean), 2);
	}
	variance = variance / elements;
	printf("Variance is: %0.2f\n", variance);

	printf("Standard Deviation is: %0.2f\n", sqrt(variance));

}
