Feature: Anonymous visitor logs in
  In order to gain reputation within the site
  As a visitor to the site
  I want to login to my account

  Background:
    Given a user account has been registered

  @wip
  Scenario: Successfully login to the site with username and password
    Given I am on the login page
	When I enter my username and password
	And I press "Sign In"
	Then I should be on the home page
	And I should be logged in

  Scenario: Successfully login to the site with email address and password
    Given I am on the login page
	When I enter my email address and password
	And I press "Sign In"
	Then I should be on the home page
	And I should be logged in
	
  Scenario: Failed login due to wrong password
  Scenario: Failed login due to incorrect username
  Scenario: Logout of the sitr