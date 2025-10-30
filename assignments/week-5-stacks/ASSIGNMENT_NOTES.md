# Assignment 5: Browser Navigation System - Implementation Notes

**Name:** Shayla Rohrer

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**

The back and forward stacks work together to create browser-like navigation by acting as the back and forward arrows in a browser near the website url.
-The back arrow (back stack) allows you to go back to a previously viewed page as long as there is one to go back to. Clicking on the back arrow would push the current page to the forward stack and pop it from the back stack, essentially moving the page you were on from the back stack to the forward stack. Now the previously viewed page is the new top of the stack for the back stack, thus replacing the "current page".
-To go forward, clicking the forward arrow would push the current page BACK to the back stack and pop it from the forward stack. Now you would be back to the original current page you started on.

## Challenges and Solutions

**Biggest challenge faced:**

I had a pretty good time with this challege. I think the hardest part was understanding what the goal was with the DisplayBackHistory method. 

**How you solved it:**

Initially, I wondered if it should have all the elements in the stack listed but once I did the DisplayForwardHistory method it made more sense how they functioned together. I ran the program and ran through back history and forward history options while displaying in between each step and was able to see that my logic made sense.

**Most confusing concept:**

The most confusing concept was just remembering what Pop and Push meant (ie remove or add item).

## Code Quality

**What you're most proud of in your implementation:**

I think my VisitUrl method is really well done and makes sense.

**What you would improve if you had more time:**

I think I would improve the format the program shows up with in the console. Theres a few little pieces that should be shifted around to look better.

## Testing Approach

**How you tested your implementation:**

I tested in between each method implementation to see that they were working and how they worked. After all were completed, I tested them together for a final time.

**Issues you discovered during testing:**

There were no real issues I found during testing. It all went smoothly.

## Stretch Features

None implemented

## Time Spent

**Total time:** 1 1/2 hours

**Breakdown:**

- Understanding the assignment: [15 minutes]
- Implementing the 6 methods: [45 minutes]
- Testing and debugging: [15 minutes]
- Writing these notes: [15 minutes]

**Most time-consuming part:** Implementing the methods took the longest because its the bulk of the assignment. I also didn't need as much time to get set up and understand the assignment as I did for previous assignments because I am now familiar with those tasks due to practice and experience.
