/** 
* Yoseph Solomon
* Feburary 18, 2015
* 
* This program is meant to simmulate algorithms and their running times by running
* a simulation of the Maximum Subsequent Sum of a set of numbers. Each algorithm 
* is separated by runtime, complexity, and methodology.
*
* To test the MSS try: {−2, 1, −3, 4, −1, 2, 1, −5, 4}. Where the MSS is {4, -1, 2, 1} = 6
* To test the algorithms try: n = 10,000
*/

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class Driver {
	public static void main(String[] args) {
		// Variables
		Scanner scan = new Scanner(System.in);
		// Class object
		Algorithms alg = new Algorithms();

		int ListSequence[];
		// for calculating simulation times
		long time;
		int sequenceSize;
		
		boolean bQuitMenu = true;
		
		// Menu
		while(bQuitMenu) {
			System.out.println("1. Calculate the Max Subsequence of a list.");
			System.out.println("2. Test Algorithms.");
			System.out.println("3. Exit.");
			int selection = scan.nextInt();
			
			switch(selection) {
			// Main purpose is to test accuracy calculating MSS, enter values by hand
			case 1:
				System.out.println("How large is the list? ");
				sequenceSize = scan.nextInt();

				// take in the entire line, delimited by commas...
				System.out.println("Enter numbers in a comma separated list");
				scan.nextLine();

				// .. and parse them into a list of strings
				String nums = scan.nextLine();
				List<String> stringListSeq = Arrays.asList(nums.split("\\s*,\\s*"));
				
				// produce an array of the same length
				ListSequence = new int[sequenceSize];

				// convert the data from the list of strings into the integer array
				for(int i = 0; i < stringListSeq.size(); i++) {
					ListSequence[i] = Integer.valueOf(stringListSeq.get(i));
				}
				
				// run appropriate algorithms on sequence
				alg.menu(ListSequence);
				break;

			// Test simmulations with a large amount of data produced from randomization
			case 2:
				System.out.println("Enter how many numbers to run: ");
				sequenceSize = scan.nextInt();

				ListSequence = new int[sequenceSize]; 
				
				// randomly produce an integer in the range [-50, 50]
				for(int i = 0; i < sequenceSize; i ++) {
					ListSequence[i] = (int)((Math.random() * 100)) - 50;
				}
				
				// run appropriate algorithms on sequence
				alg.menu(ListSequence);
				break;

			// Exit program
			case 3:
				System.out.println("Exiting...");
				bQuitMenu = false;
				System.exit(0);
				break;

			// Invalid input handling
			default:
				System.out.println("Invalid.");
				break;
			}
		}
	}
}
