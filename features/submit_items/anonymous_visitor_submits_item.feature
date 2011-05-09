Feature: Anonymous visitor submits item
  In order to share informative articles and news stories with fellow developers
  As a visitor to the site
  I want to submit a new item

  Scenario: Successful submission
    Given I am on the submit item page
    And I enter complete the mandatory fields
	When I click "Post Item"
    Then I should see the message "Thank-you for submitting a new item, it is now pending approval."