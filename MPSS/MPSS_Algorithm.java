/** 
* Yoseph Solomon
* March 11, 2015
* 
* This class houses the algorithms and functions necessary for data collection. It also 
* details a small analysis of each algorithm and the relevant complexity
*/

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class MPSS_Algorithm {
	// Variables
	Scanner scan = new Scanner(System.in);

	// report times for simulations
	long time;
	
	// the MPSS for the left and right lists
	int mid = 0;
	int count = 0;
	int[] leftSum;
	int[] rightSum;
	int[] listSequence;
	int dataSize;
	
	boolean f = true;
	
	// function generates an array of randomly generated numbers for simulation
	public void randomArray() {
		System.out.println("Enter the size: ");
		dataSize = scan.nextInt();

		listSequence = new int[dataSize]; 
		
		// fills list with numbers
		for(int i = 0; i < dataSize; i ++) {
			listSequence[i] = (int) ((Math.random() * 500) % 500 + 1) - 250;
		}
		
		// run algorithms on data and report their time as well as sum
		System.out.println("Running Sophmore...");
		time = System.nanoTime();
		System.out.println("MPSS Sophmore is " + mpss_sophmore(listSequence, 0, listSequence.length - 1));
		reportTime(time);
		System.out.println("Running Junior...");
		time = System.nanoTime();
		System.out.println("MPSS Junior is " + mpss_junior(listSequence, 0, listSequence.length - 1));
		reportTime(time);
	}
	
	// method allows user to input a sequence by hand to find MPSS
	public void createArray() {
		System.out.println("How large is the list? ");
		dataSize = scan.nextInt();

		System.out.println("Enter numbers in a comma separated list");
		scan.nextLine();

		// convert comma delineation into int array
		String nums = scan.nextLine();
		List<String> stringListSeq = Arrays.asList(nums.split("\\s*,\\s*"));
		
		listSequence = new int[dataSize];
		
		// fill array with appropriate integer data from List
		for(int i = 0; i < stringListSeq.size(); i++) {
			listSequence[i] = Integer.valueOf(stringListSeq.get(i));
		}
		
		// perform algorithms on data reporting time and sum
		System.out.println("Running Sophmore...");
		time = System.nanoTime();
		System.out.println("MPSS Sophmore is " + mpss_sophmore(listSequence, 0, listSequence.length - 1));
		reportTime(time);
		System.out.println("Running Junior...");
		time = System.nanoTime();
		System.out.println("MPSS Junior is " + mpss_junior(listSequence, 0, listSequence.length - 1));
		reportTime(time);
	}
	
	// Standard approach algorithm used to compute MPSS in O(n^2) time
	public int mpss_sophmore(int[] listSequence, int left, int right) {
		// sum base case
		int sum = 500; 
		int tally;

		// run every element..
		for(int i = 0; i < listSequence.length; i++) {
			// reset tally
			tally = 0;

			// ..against every other
			for(int j = i; j < listSequence.length; j++) {
				tally += listSequence[j];
				// keep if smaller and positive
				if(tally < sum && tally > 0) {
					sum = tally;
				}	
			}
		}
		return sum;
	}
	
	// Advanced approach to solving the MPSS using recursion with a complexity of O(n log n).
	// employed by breaking down the list into smaller sublists and performing operations on them.
	public int mpss_junior(int listSequence[], int left, int right) {
		// base case
		if(right - left < 2) {
			return min(listSequence[right], listSequence[left]);
		} 
		
		// calculate the middle
		int mid = (left + right) / 2;

		// A chain recursive call, somewhat stack intensive, that finds the minimum of all 3 sections
		return min(min(mpss_junior(listSequence, left, mid), mpss_junior(listSequence, mid + 1, right)), mpss_mid(listSequence, mid, left, right));
	}
	
	// This function serves to calculate the MPSS of the left and right lists
	public int mpss_mid(int[] listSequence, int mid, int left, int right)
	{	
		// Left and Right subarrays
		leftSum = new int[mid - left + 1];
		rightSum = new int[right - mid];
		
		int tally = 0;
		count = 0;
	
		// calculate left sums..
		for(int i = mid; i >= left; i--) {
			tally += listSequence[i];
			leftSum[count] = tally;
			count++;
		}
		// .. then reset tally and count for..
		tally = 0;
		count = 0;
		
		// .. right sums
		for(int i = mid + 1; i <= right; i++) {
			tally += listSequence[i];
			rightSum[count] = tally;
			count++;
		}
		
		// execute a quicksort on both the left and right lists
		Arrays.sort(leftSum);
		Arrays.sort(rightSum);
		
		// debug statement to see subarrays
		//System.out.println(Arrays.toString(leftSum));
		//System.out.println(Arrays.toString(rightSum));
		
		// left right sum
		int midSum = 0;
		// base case value
		int min = 500;
		int index = 0, maxIndex = rightSum.length - 1;

		// find the MPSS of the left and right
		while(index < leftSum.length && maxIndex >= 0) {
			midSum = leftSum[index] + rightSum[maxIndex];

			// index appropriately
			if(midSum <= 0) {
				index++;
			} else {
				maxIndex--;
			} 

			// keep the min positive sum of the mid section
			if(midSum < min && midSum > 0) {
				min = midSum;
			}
		}
		// used for debugging more complicated sequences, reports middle sum
		//System.out.println("mid is " + min);
		return min;
	}
	
	// min of three values
	public int min(int a, int b, int c) {
		return min(min(a, b), c);
	}
	
	// custom min of two values function
	public int min(int a, int b) {
		// if both negative, return "base case" number
		if(a <= 0 && b <= 0) {
			return 500;
		}

		// if both positive, return the smaller
		if(a > 0 && b > 0) {
			if(a < b) { 
				return a; 
			} else {
				return b;
			}

		// if one is positive and the other negative
		} else if(a > 0 && b <= 0) {
			if(a + b > 0 && a + b < a) {
				return a + b;
			} else { 
				return a;
			}
		} else if(a <= 0 && b > 0) {
			if(a + b > 0 && a + b < b) {
				return a + b; 
			} else {
				return b;
			}

		// finally again the easily replacable "base case" number
		} else {
			return 500;
		}
	}
	
	// function to report times
	public void reportTime(long time) { 
		double mSec = (double)((System.nanoTime() - time) / 1000000);
		double sec =  mSec / 1000;
	
		if(mSec > 1000) {
			System.out.println("Runtime: " + sec + " seconds.");
		} else {
			System.out.println("Runtime: " + mSec + " milliseconds");
		}
	}
}