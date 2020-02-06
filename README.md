# UL-Test

Please Clean and Rebuild the solution.
You can run the unit tests in the test explorer.

I have developed two implementations of the parser.

Both implementations read the input string expression left to right parse it and create a list of tokens which will be iterated again to evalute final result.

BLL project
1. Parsing expressions not containing brackets using rules of operator precedence. Got inspiaration from a wikipedia article
https://en.wikipedia.org/wiki/Operator-precedence_parser
2. Implemented a seperate parser to parse expressions containing brackets. Where each expression is broken in to terms (+ or -), which in turn can be broken in to factors (* or /) and final layer being digits or an expression again within brackets. I reached a bit about writing parsers and settled on the below grammer.

Expression := Term { ("+" | "-") Term }
Term       := Factor { ( "*" | "/" ) Factor }
Factor     := Number | "(" Expression ")"

BLL-Tests project - MSTests for the two parsers.

The UI is a basic ASP.NET Web Forms page. The BLL method calls are coupled to the UI for this revision. You can select which parser to use from a drop down list and evaluate expression.