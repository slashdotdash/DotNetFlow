Feature: Anonymous visitor submits item
  In order to share informative articles and news stories with fellow developers
  As a visitor to the site
  I want to submit a new item

  Scenario: Successful submission
    Given I am on the submit item page
	#When I fill in "Your Name" with "Ben Smith"
    When I fill in the following:
	  | Your Name		| Ben Smith											|
	  | Title			| Fantastic new .NET resource						|
	  | Content         | Check out [dotnetflow](http://www.dotnetflow.com) |
	And I press "Submit Item"
	Then I should be on the home page
    And I should see the message "Thank-you for submitting a new item, it is now pending approval."