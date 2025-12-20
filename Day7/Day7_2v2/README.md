# Advent of Code 2025, day 7 2nd task, new approach

[Instructions](https://adventofcode.com/2025/day/7)

Instead of simulating all the rays, we can go row by row and count how many rays pass through a column. 
We can do this because the number of columns is finite.
For example, if we already have 5 rays in the previous row at position n and we encounter a splitter ('^' character) in the current row, this means that in the current row at positions n-1 and n+1 we add 5 rays to the value that was in the previous row at positions n-1 and n+1


The optimisation here is that, if two rays from different columns are redirected to the same column due to a splitter, We don't need to calculate what happens next to each ray, because the same thing will happen to both.
