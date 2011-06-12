Then /^I should be redirected to view the submitted item$/ do
  
end

Then /^I should see the message "([^"]*)"$/ do |message|
  Then %{I should see "#{message}"}
end