Given /^I complete the mandatory fields$/ do
  pending # express the regexp above with the code you wish you had
end

Then /^I should be redirected to view the submitted item$/ do
  
end

Then /^I should see the message "([^"]*)"$/ do |message|
  Then %{I should see "#{message}" within ".message"}
end