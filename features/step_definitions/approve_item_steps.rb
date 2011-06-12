Given /^an anonymous visitor has submitted an item$/ do
  @name = Faker::Name.name
  @title = Faker::Lorem.sentence
  @content = Faker::Lorem.paragraph

  Given %{I am on the submit item page}
  Given %{I fill in "Your Name" with "#{@name}"}
  Given %{I fill in "Title" with "#{@title}"}
  Given %{I fill in "Content" with "#{@content}"}
  Given %{I press "Submit Item"}
end

Given /^I am logged in as an approver$/ do
  Given %{I am on the login page}
  Given %{I fill in "E-mail" with "approver@dotnetflow.com"}
  Given %{I fill in "Password" with "approval"}
  Given %{I press "Sign In"}
end

When /^I approve the submission$/ do
  When %{I press "Approve" within "table#pending-approval tr:last"}
end

Then /^the submitted item should be removed from the pending approval list$/ do
  pending # express the regexp above with the code you wish you had
end

Then /^the approved item should appear on the home page$/ do
  pending # express the regexp above with the code you wish you had
end

Then /^the published date should be set as today$/ do
  pending # express the regexp above with the code you wish you had
end


#Given /^I complete the mandatory fields$/ do
#  pending # express the regexp above with the code you wish you had
#end

#Then /^I should be redirected to view the submitted item$/ do
#  
#end

#Then /^I should see the message "([^"]*)"$/ do |message|
#  Then %{I should see "#{message}"}
#end