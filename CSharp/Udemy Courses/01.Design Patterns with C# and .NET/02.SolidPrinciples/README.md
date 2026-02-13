# Introduction 
The purpose of this section is to study SOLID principles.

# What we learned here
- Open/Closed Principle
- Creating products and then 2 filters, filtering by size and color.
- Then someone request to filter by both size and color. Do we violate the principle of ProductFilter be close to modifications?
- If we modify ProductFilter, we violate the principle because the principle says that the class should only be opened for extensions but closed for modifications.