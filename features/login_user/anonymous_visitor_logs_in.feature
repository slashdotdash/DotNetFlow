Feature: Anonymous visitor logs in
  In order to gain reputation within the site
  As a registered user of the site
  I want to login to my account

  Background:
    Given a user account has been registered

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
    Given I am on the login page
	When I enter my email address and the wrong password
	And I press "Sign In"
	Then I should see the login failed error message
	And I should not be logged in

  Scenario: Failed login due to incorrect username
    Given I am on the login page
	When I enter an incorrect username and password
	And I press "Sign In"
	Then I should see the login failed error message
	And I should not be logged in
	
  Scenario: Logout of the site