Feature: Approver publishes submitted item
  In order for submitted items to appear on the site
  As a logged in user with approval permission
  I want to approve submitted items

  @wip
  Scenario: Approve submission
    Given an anonymous visitor has submitted an item
	And I am logged in as an approver
	And I am on the submissions pending approval page
	When I approve the submission
	#Then I should see the message "Submission approved"
	And the submitted item should be removed from the pending approval list
	And the approved item should appear on the home page
	And the published date should be set as today

  Scenario: Reject submission
  Scenario: Edit and approve submission