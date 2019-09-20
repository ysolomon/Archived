/** 
* Yoseph Solomon
* Feburary 18, 2015
* 
* This class houses the algorithms and functions necessary for data collection. It also 
* details a small analysis of each algorithm and the relevant complexity. 
*/

import java.util.Scanner;

public class Algorithms {
	// Variables
	Scanner scan = new Scanner(System.in);

	int max;
	int sum;
	// for calculating how long each algorithm takes
	long time;

	// Reports the time for an algorithm upon completion
	public void reportTime(long time) { 
		// Calculate time in miliseconds and seconds
		double mSec = (double)((System.nanoTime() - time) / 1000000);
		double sec =  mSec / 1000;
		
		// report the time in the appropriate units
		if(mSec > 1000) {
			System.out.println("Runtime: " + sec + " seconds.");
		} else {
			System.out.println("Runtime: " + mSec + " milliseconds");
		}
	}
	
	// function that serves as a hub to run all algorithms
	public void menu(int listSequence[]) {
		System.out.println("Which Algorithms would you like to run? ");
		String order = scan.nextLine();
		
		// executes algorithms based on number put in
		for(int i = 0; i < order.length(); i++) {
			// begin counting time for simulations
			time = System.nanoTime();

			if(order.charAt(i) == '1') {
				System.out.println("Running Freshman...");
				System.out.println("Sum: " + Freshman(listSequence));
				reportTime(time);
			}
			else if(order.charAt(i) == '2') {
				System.out.println("Running Sophmore...");
				System.out.println("Sum: " + Sophmore(listSequence));
				reportTime(time);
			}
			else if(order.charAt(i) == '3') {
				System.out.println("Running Junior...");
				System.out.println("Sum: " + Junior(listSequence, 0, listSequence.length - 1));
				reportTime(time);
			}
			else if(order.charAt(i) == '4') {
				System.out.println("Running Senior...");
				System.out.println("Sum: " + Senior(listSequence));
				reportTime(time);
			}
		}
	}
	
		
	// Algorithms
	// Freshman level algorithm implements a nested for structure with a time of 
	// n * n * n for a complexity of O(n^3)
	public int Freshman(int listSequence[]) {	
		max = 0;

		// calculate a sum for every element.. 
		for(int i = 0; i < listSequence.length; i++) {
			// .. against every other
			for(int j = i; j < listSequence.length; j++) {
				// reset sum
				sum = 0;
				
				// try each possibility..
				for(int k = i; k <= j; k++) {
					sum += listSequence[k];
				}
				
				// .. keep the largest
				if(sum > max) {
					max = sum;
				}
			}
		}
		return max;
	}
	
	// Sophmore level algorithm like Freshman, improves upon its predecessor by
	// performing a running sum inside the first nested loop and using a comparison
	// these adjustments yield a time of n * n for n O(n^2)
	public int Sophmore(int listSequence[]) {
		max = 0; 
		
		for(int i = 0; i < listSequence.length; i++) {
			// reset the sum for each number
			sum = 0;

			for(int j = i; j < listSequence.length; j++) {
				sum += listSequence[j];

				// keep largest
				if(sum > max) {
					max = sum;
				}
			}
		}
		return max;
	}

	// Junior level algorithm varies greatly from the previous by its use of recursion.
	// Continuously halves and calls itself using the middle of the list finding the largest 
	// of each subsequence it creates. Evaluates each sequence saving the largest MSS and 
	// returns back to the main call once base cases have been reached.
	//
	// The complexity of the Junior algorithm is O(n Log n), taking every element n against
	// every half (log n)
	public int Junior(int listSequence[], int left, int right) {
		// Base Case, for arrays of size 2, none, or negative
		if(left == right) {
			if(listSequence[left] > 0) {
				return listSequence[left];
			} else {
				return 0;
			}
		}

		// determine center of list 
		int mid = (left + right) / 2;
		
		// store a maximum value from each subsequence
		int lMax = Junior(listSequence, left, mid);
		int rMax = Junior(listSequence, mid + 1, right);
		
		// determine the largest sum of the left and right portions
		int tempSum = 0, lSum = 0, rSum = 0;
		
		// calculate left sum, counting from mid to farthest left
		for(int i = mid; i >= left; i--) {
			tempSum += listSequence[i];

			if(tempSum > lSum) {
				lSum = tempSum;
			}
		}

		// reset
		tempSum = 0;

		// calculate right sum, counting up
		for(int i = mid + 1; i <= right; i++) {
			tempSum += listSequence[i];

			if(tempSum > rSum) {
				rSum = tempSum;
			}
		}
		
		// max of 3
		return max(lMax, rMax, lSum + rSum);
	}
	
	// Max of 3
	private int max(int lMax, int rMax, int leftRightSum)
	{
		if(lMax > rMax) {
			if(lMax > leftRightSum) {
				return lMax;
			} else {
				return leftRightSum;
			}
		} else if(rMax > leftRightSum) {
			return rMax;
		} else {
			return leftRightSum;
		}
    }
	
	// Senior level algorithm makes use of simple logic, computes and only keeps sum
	// if it is larger, otherwise return 0 and move on, with a Complexity is O(n)
	public int Senior(int listSequence[]) {
		max = 0;
		sum = 0;
		
		// traverse list..
		for(int i = 0; i < listSequence.length; i++) {
			// ..add to running sum..
			sum += listSequence[i];
			
			// .. keep if it is larger..
			if(sum > max) {
				max = sum;
			}
			// .. if not, ignore it
			else if(sum < 0) {
				sum = 0;
			}
		}
		return max;
	}
}
