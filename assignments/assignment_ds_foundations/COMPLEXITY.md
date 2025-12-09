Structure:	Operation:		Big-O(Avg):	One-sentence Rationale:
Array		Access by index		O(1)		Index same regardless of array size so no need to iterate
Array		Search(unsorted)	O(n)		Must iterate through a portion of the array
List<T>		Add at end		O(n)		Has to traverse list to find end before adding
List<T>		Insert at index		O(n)		Has to shift following elements when inserting
Stack<T>	Push/Pop/Peek		O(1)		Doesn't need to shift elements, start at end or beginning
Queue<T>	Enqueue/Dequeue/Peek	O(1)		Doesn't need to shift elements, start at end or beginning	
Dictionary<K,V> Add/Loopup/Remove	O(1)		Hash function to directly find location of element, regardless of size
HashSet<T>	Add/Contains/Remove	O(1)		Same as dictionary 
