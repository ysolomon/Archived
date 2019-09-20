/** 
* Yoseph Solomon
* March 11, 2015
* 
* The purpose of this program is to compare two algorithms, each for calculating the
* Minimum Positive Subsequent Sum (MPSS). One is a standard approach while the other
* will be a Divde and Conquer algorithm.
*
* To test the MPSS try: {-50, 82, -49, 25, -36, 40, -58}. Where the MPSS is {-36, 40} = 4
* To test the algorithms try: n = 10,000
*/

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class Driver {
	public static void main(String[] args) {
		// Variables
		Scanner scan = new Scanner(System.in);

		MPSS_Algorithm alg = new MPSS_Algorithm();
		
		boolean bQuitMenu = true;
		
		while(bQuitMenu) {
			System.out.println("1. Run Junior and Sophmore Algorithms");
			System.out.println("2. Run Test Values");
			System.out.println("3. Exit");
			
			int selection = scan.nextInt();
			
			switch(selection)
			{
			// for testing accuracy of the algorithm by hand
			case 1:
				alg.createArray();
				break;

			// run simulations on bulk randomly generated data
			case 2:
				alg.randomArray();
				break;

			// exit the program
			case 3:
				System.out.println("Exiting...");
				bQuitMenu = false;
				System.exit(0);
				break;

			// for input error handling
			default:
				System.out.println("Invalid.");
				break;
			}
		}
	}
}
