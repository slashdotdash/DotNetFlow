Feature: Anonymous visitor views published items
  In order to read informative articles and news stories shared by fellow developers
  As a visitor to the site
  I want to view published items

  Background:
    Given a user account with approval permission has been registered
	And I am logged in as an approver	

  Scenario: View list of items on homepage
  Scenario: View single published item
  
  Scenario: Search engine friendly URLs for items
	Given an item with the title "Welcome to dotnetflow" has been published
	When I visit the URL "/items/welcome-to-dotnetflow"
	Then I should see the published item

  Scenario: Paginate through list of items on homepage